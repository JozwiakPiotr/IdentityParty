# IdentityParty

This project is OpenID provider framework for ASP.Net.

## Features
### Must have
* CRUD:
    - [ ] end user
    - [ ] clients
    - [ ] scopes
    - [ ] grants
* Authorize endpoint
    - [ ] Authorization Code Grant
    - [ ] Implicit Grant
    - [ ] Resource Owner Password Credentials Grant
    - [ ] Client Credentials Grant
* Token endpoint
* Frontend
    - [ ] end user management
    - [ ] client management
* Library API
    - [ ] Install IdentityParty to ASP.Net app
* OpenID implementation
    - [ ] OpenID Connect Core
    - [ ] OpenID Connect Discovery
    - [ ] OpenID Connect Dynamic Client Registration
### Nice to have
* Integration with ASP.Net Core Identity

TODO:
- [ ] [Configure OAuth authorization code: storing, max lifetime, boudning, revoking tokens](https://datatracker.ietf.org/doc/html/rfc6749#section-4.1.2)

> REQUIRED.  The authorization code generated by the
         authorization server.  The authorization code MUST expire
         shortly after it is issued to mitigate the risk of leaks.  A
         maximum authorization code lifetime of 10 minutes is
         RECOMMENDED.  The client MUST NOT use the authorization code
         more than once.  If an authorization code is used more than
         once, the authorization server MUST deny the request and SHOULD
         revoke (when possible) all tokens previously issued based on
         that authorization code.  The authorization code is bound to
         the client identifier and redirection URI.
- [ ] learn OpenID Connect Core
- [ ] learn OpenID Connect Discovery
- [ ] learn OpenID Connect Dynamic Client Registration
- [ ] support ASP.Net Core LTS Versions (8,6) 
- [ ] design projects structure
- [ ] add .editorconfig file, docs:https://learn.microsoft.com/en-us/dotnet/fundamentals/code-analysis/code-style-rule-options