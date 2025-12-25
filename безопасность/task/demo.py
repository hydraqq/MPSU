import time
from user import InMemoryUserStorage
from auth import register_user, verify_credentials


def demo():
    print("=" * 70)
    print("Password Security Demo - Argon2 + Delay Growth + Login Logging")
    print("=" * 70)
    
    store = InMemoryUserStorage()
    
    print("\n[1] Registering user 'alice' with password 'MySecurePass123!'")
    alice = register_user(store, "alice", "alice@example.com", "MySecurePass123!")
    print(f"    Username: {alice.username}")
    print(f"    Email: {alice.email}")
    print(f"    Password hash (Argon2): {alice.password_hash[:50]}...")
    print(f"    Initial delay: {alice.backoff_seconds}s")
    
    print("\n[2] Testing failed login attempts with delay growth:")
    print("    Formula: delay = 1.5^n + 1, where n = attempt number")
    print()
    
    for attempt in range(1, 6):
        print(f"    Attempt {attempt}: Wrong password 'WrongPass123!'")
        start = time.time()
        result = verify_credentials(store, "alice", "WrongPass123!")
        elapsed = time.time() - start
        
        user = store.get_user("alice")
        expected_delay = (1.5 ** attempt) + 1.0
        
        print(f"      Result: {result}")
        print(f"      Expected delay: {expected_delay:.2f}s")
        print(f"      Actual delay: {elapsed:.2f}s")
        print(f"      Stored backoff_seconds: {user['backoff_seconds']:.2f}s")
        print()
    
    print("[3] Successful login with correct password:")
    print("    Password: 'MySecurePass123!'")
    start = time.time()
    result = verify_credentials(store, "alice", "MySecurePass123!")
    elapsed = time.time() - start
    
    user = store.get_user("alice")
    print(f"    Result: {result}")
    print(f"    Response time: {elapsed:.2f}s")
    print(f"    Delay after success: {user['backoff_seconds']:.2f}s (reset)")
    print(f"    Password migrated to Argon2: Yes")
    print(f"    New hash: {user['password_hash'][:50]}...")
    print(f"    Last login: {user['last_login']}")
    
    print("\n[4] LOGIN ATTEMPT LOG:")
    print("-" * 70)
    print(f"{'#':<3} {'Timestamp':<30} {'Status':<10} {'Delay(s)':<12}")
    print("-" * 70)
    
    for idx, attempt in enumerate(user['login_attempts'], 1):
        status = "SUCCESS" if attempt['success'] else "FAILED"
        timestamp = attempt['timestamp'].split('T')[1][:8]
        delay = f"{attempt['delay_applied']:.2f}" if attempt['delay_applied'] > 0 else "0.00"
        print(f"{idx:<3} {timestamp:<30} {status:<10} {delay:<12}s")
    
    print("-" * 70)
    print(f"Total attempts: {len(user['login_attempts'])}")
    print(f"Failed: {sum(1 for a in user['login_attempts'] if not a['success'])}")
    print(f"Success: {sum(1 for a in user['login_attempts'] if a['success'])}")
    
    print("\n" + "=" * 70)
    print("Demo Complete!")
    print("=" * 70)


if __name__ == "__main__":
    demo()
