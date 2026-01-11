import { dotnet } from './_framework/dotnet.js'
import { UserManager } from 'https://cdn.jsdelivr.net/npm/oidc-client-ts@2.1.0/dist/oidc-client-ts.min.js';

const is_browser = typeof window != "undefined";
if (!is_browser) throw new Error(`Expected to be running in a browser`);

const dotnetRuntime = await dotnet
    .withDiagnosticTracing(false)
    .withApplicationArgumentsFromQuery()
    .create();

const userManager = new UserManager({
    authority: "http://127.0.0.1:8990/",
    client_id: "schoolAppWeb",
    redirect_uri: window.location.origin + "/callback",
    response_type: "code",
    scope: "openid profile api",
    post_logout_redirect_uri: window.location.origin,
    automaticSilentRenew: true,
    silent_redirect_uri: window.location.origin + "/silent-renew.html"
});

const config = dotnetRuntime.getConfig();

await dotnetRuntime.runMain(config.mainAssemblyName, [globalThis.location.href]);

export async function loginWithOidc() {
    const user = await userManager.signinPopup();
    return user.access_token;
}

userManager.events.addSilentRenewError((err) => console.error("Silent renew error", err));

window.loginWithOidc = loginWithOidc;
