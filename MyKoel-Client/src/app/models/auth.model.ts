export class AuthModel {
  authToken: string;
  refreshToken: string;
  expiresIn: Date;
  refreshExpiryInMins: Date;

  setAuth(auth: AuthModel) {
    this.authToken = auth.authToken;
    // this.refreshToken = auth.refreshToken;
    this.expiresIn = auth.expiresIn;
    // this.refreshExpiryInMins = auth.refreshExpiryInMins;
  }
}
