import { logger } from "../utils/Logger";
import Pop from "../utils/Pop";
import { api } from "./AxiosService";
import { AppState } from "../AppState.js";

class VaultsService {
    async getAllProfileVaults(profileId) {
        try {
            const res = await api.get('api/profiles/' + profileId + '/vaults')
            logger.log('[Getting profile vaults]', res.data)
            AppState.profileVaults = res.data
        } catch (error) {
            logger.error(error)
            Pop.error(error.message)
        }
    }
}
export const vaultsService = new VaultsService()