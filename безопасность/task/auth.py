import hashlib
import time
import math
from datetime import datetime
from passlib.context import CryptContext
from validation import validate_password
from user import User, UserStorage


ctx = CryptContext(schemes=["argon2"], deprecated="auto")


def register_user(storage: UserStorage, username: str, email: str, password: str) -> User:
    if User.exists(storage, username):
        raise ValueError("User already exists")

    validate_password(password)

    argon2_hash = ctx.hash(password)
    user = User(username=username, email=email, password_hash=argon2_hash, backoff_seconds=0.0)
    user.save(storage)
    return user


def verify_credentials(storage: UserStorage, username: str, password: str) -> bool:
    user = User.load(storage, username)
    if user is None:
        return False

    is_md5 = len(user.password_hash) == 32 and all(c in "0123456789abcdef" for c in user.password_hash)
    
    if is_md5:
        md5_hex = hashlib.md5(password.encode("utf-8")).hexdigest()
        correct = user.password_hash == md5_hex
    else:
        correct = ctx.verify(password, user.password_hash)
    
    if correct:
        if is_md5:
            user.password_hash = ctx.hash(password)
        
        user.backoff_seconds = 0.0
        user.last_login = datetime.now().isoformat()
        
        attempt_log = {
            "timestamp": user.last_login,
            "success": True,
            "delay_applied": 0.0
        }
        user.login_attempts.append(attempt_log)
        user.save(storage)
        return True
    else:
        if user.backoff_seconds == 0.0:
            n = 1
        else:
            n = round(math.log(user.backoff_seconds - 1.0, 1.5))
            n += 1
        
        delay = (1.5 ** n) + 1.0
        user.backoff_seconds = delay
        
        timestamp = datetime.now().isoformat()
        attempt_log = {
            "timestamp": timestamp,
            "success": False,
            "delay_applied": delay
        }
        user.login_attempts.append(attempt_log)
        user.save(storage)
        
        time.sleep(delay)
        return False
