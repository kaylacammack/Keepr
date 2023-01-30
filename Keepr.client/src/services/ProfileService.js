import { AppState } from "../AppState";
import { Profile } from "../models/Profile";
import { logger } from "../utils/Logger";
import { api } from "./AxiosService";

class ProfileService {
    async getProfileById(profileId) {
        const res = await api.get('api/profiles/' + profileId)
        logger.log(['Getting profile', res.data])
        AppState.profile = new Profile(res.data)
    }
}
export const profileService = new ProfileService()