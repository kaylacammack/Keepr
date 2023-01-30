import { logger } from "../utils/Logger";
import Pop from "../utils/Pop";
import { api } from "./AxiosService";
import { AppState } from "../AppState.js";
import { Keep } from "../models/Keep.js";

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

    async GetKeepsByVaultId(vaultId) {
        try {
            const res = await api.get('api/vaults/' + vaultId + '/keeps')
            logger.log('[Getting vault keeps]', res.data)
            AppState.vaultKeeps = res.data
        } catch (error) {
            logger.error(error)
            Pop.error(error.message)
        }
    }
}
export const vaultsService = new VaultsService()