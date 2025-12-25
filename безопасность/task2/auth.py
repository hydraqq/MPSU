from typing import Any, Tuple
from datetime import datetime, timezone
import crypto
import storage
import user as user_module

def record_token(payload: dict[str, Any]) -> None:
    db = storage.load_tokens()
    jti = payload["jti"]
    db["tokens"].append({
        "jti": jti,
        "typ": payload.get("typ"),
        "sub": payload.get("sub"),
        "exp": payload.get("exp"),
        "revoked": False
    })
    storage.save_tokens(db)

def revoke_by_jti(jti: str) -> None:
    db = storage.load_tokens()
    for token in db["tokens"]:
        if token["jti"] == jti:
            token["revoked"] = True
            break
    storage.save_tokens(db)

def is_revoked(jti: str) -> bool:
    db = storage.load_tokens()
    for token in db["tokens"]:
        if token["jti"] == jti:
            return token["revoked"]
    return False

def is_expired(exp: int) -> bool:
    now = int(datetime.now(timezone.utc).timestamp())
    return now > exp

def login(username: str, password: str) -> Tuple[str, str]:
    u = user_module.get_user(username)
    if u is None or not user_module.verify_password(u, password):
        raise ValueError("invalid credentials")
    
    access_token, access_payload = crypto.issue_access(username)
    refresh_token, refresh_payload = crypto.issue_refresh(username)
    
    record_token(access_payload)
    record_token(refresh_payload)
    
    return access_token, refresh_token

def verify_access(access: str) -> dict[str, Any]:
    payload = crypto.decode(access)
    
    if payload.get("typ") != "access":
        raise ValueError("wrong token type")
    
    if is_revoked(payload["jti"]):
        raise ValueError("token revoked")
    
    if is_expired(payload["exp"]):
        raise ValueError("token expired")
    
    return payload

def refresh_pair(refresh_token: str) -> Tuple[str, str]:
    payload = crypto.decode(refresh_token)
    
    if payload.get("typ") != "refresh":
        raise ValueError("wrong token type")
    
    if is_revoked(payload["jti"]):
        raise ValueError("token revoked")
    
    if is_expired(payload["exp"]):
        raise ValueError("token expired")
    
    revoke_by_jti(payload["jti"])
    
    sub = payload["sub"]
    access_token, access_payload = crypto.issue_access(sub)
    new_refresh_token, new_refresh_payload = crypto.issue_refresh(sub)
    
    record_token(access_payload)
    record_token(new_refresh_payload)
    
    return access_token, new_refresh_token

def revoke(token: str) -> None:
    try:
        payload = crypto.decode(token)
        revoke_by_jti(payload["jti"])
    except Exception:
        raise ValueError("invalid token")

def introspect(token: str) -> dict[str, Any]:
    try:
        payload = crypto.decode(token)
        active = (not is_revoked(payload["jti"])) and (not is_expired(payload["exp"]))
        return {
            "active": active,
            "sub": payload.get("sub"),
            "typ": payload.get("typ"),
            "exp": payload.get("exp"),
            "jti": payload.get("jti"),
        }
    except Exception:
        return {"active": False, "error": "invalid_token"}
