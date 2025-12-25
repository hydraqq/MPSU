import auth
import user
import storage
import crypto

def demo():
    storage.clear_data()
    
    print("=" * 70)
    print("JWT Token System Demo - HS256 Authentication")
    print("=" * 70)
    
    print("\n[1] Register user")
    user.register_user("demo_user", "demo@example.com", "SecurePass123!")
    print("    User registered: demo_user")
    
    print("\n[2] Login and get tokens")
    access_token, refresh_token = auth.login("demo_user", "SecurePass123!")
    access_payload = crypto.decode(access_token)
    refresh_payload = crypto.decode(refresh_token)
    print(f"    Access Token JTI: {access_payload['jti']}")
    print(f"    Refresh Token JTI: {refresh_payload['jti']}")
    
    print("\n[3] Verify access token (me)")
    payload = auth.verify_access(access_token)
    print(f"    Subject: {payload['sub']}")
    print(f"    Type: {payload['typ']}")
    print(f"    Scope: {payload['scope']}")
    print(f"    JTI: {payload['jti']}")
    print(f"    Expires: {payload['exp']}")
    
    print("\n[4] Introspect token (active check)")
    introspect_result = auth.introspect(access_token)
    print(f"    Active: {introspect_result['active']}")
    print(f"    Subject: {introspect_result['sub']}")
    print(f"    Type: {introspect_result['typ']}")
    
    print("\n[5] Refresh token rotation")
    new_access, new_refresh = auth.refresh_pair(refresh_token)
    new_access_payload = crypto.decode(new_access)
    new_refresh_payload = crypto.decode(new_refresh)
    print(f"    New Access Token JTI: {new_access_payload['jti']}")
    print(f"    New Refresh Token JTI: {new_refresh_payload['jti']}")
    print(f"    Old Refresh revoked: Yes")
    
    print("\n[6] Try to use old refresh (should fail)")
    try:
        auth.refresh_pair(refresh_token)
        print("    ERROR: Old token should be revoked!")
    except Exception as e:
        print(f"    Expected error: {str(e)}")
    
    print("\n[7] Revoke access token")
    auth.revoke(new_access)
    print(f"    Token revoked: Yes")
    
    print("\n[8] Introspect revoked token")
    revoked_result = auth.introspect(new_access)
    print(f"    Active: {revoked_result['active']} (should be False)")
    
    print("\n" + "=" * 70)
    print("Demo Complete!")
    print("=" * 70)

if __name__ == "__main__":
    demo()
