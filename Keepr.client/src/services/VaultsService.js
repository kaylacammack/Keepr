import { logger } from "../utils/Logger";
import Pop from "../utils/Pop";
import { api } from "./AxiosService";
import { AppState } from "../AppState.js";
import { Vault } from "../models/Vault";

class VaultsService {
    async createVault(vaultData) {
        try {
            const res = await api.post('api/vaults', vaultData)
            AppState.vaults = [new Vault(res.data), ...AppState.vaults]
            
        } catch (error) {
            logger.error(error)
            Pop.error(error.message)
        }
    }
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

    async getVaultById(vaultId) {
        const res = await api.get('api/vaults/' + vaultId)
        AppState.activeVault = res.data
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

    async deleteVault(vaultId) {
        await api.delete('api/vaults/' + vaultId)
        AppState.accountVaults = AppState.accountVaults.filter(v => v.id != vaultId)
    }
}
export const vaultsService = new VaultsService()