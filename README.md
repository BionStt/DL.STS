# DL.STS
A security token service built using ASP.NET Core and IdentityServer4. It uses ASP.NET Core Identity as user store, and Entity Framework Core as configuration and operational store.

## Add Signing Credential
For local development, you can use the built-in `.AddDeveloperSigningCredential()` to create temporary key material at startup time. But for production, we need to pass in either a self-signed certificate or a certificate from the certificate store.

We might as well generate a self-signed one now.

If you're running Windows 10, you can run this command on your administrative power shell:

    New-SelfSignedCertificate 
        -Type Custom 
        -Subject "CN=XXXX" 
        -TextExtension @("2.5.29.37={text}1.3.6.1.5.5.7.3.3") 
        -KeyUsage DigitalSignature 
        -KeyAlgorithm RSA 
        -KeyLength 2048 
        -CertStoreLocation "Cert:\LocalMachine\My" 

Replace XXXX with whatever common name you want.

Then copy the thumb print from the self-signed certificate and replace the one in `appsettings.json`:

    {
        ...,
        "ThumbPrint": "XXXX"
    }
