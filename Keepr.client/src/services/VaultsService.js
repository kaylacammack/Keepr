import { logger } from "../utils/Logger";
import Pop from "../utils/Pop";
import { api } from "./AxiosService";
import { AppState } from "../AppState.js";
import { Keep } from "../models/Keep.js";
import { Vault } from "../models/Vault";

class VaultsService {
    async createVault(vaultData) {
        try {
            const res = await api.post('api/vaults', vaultData)
            AppState.vaults.push(new Vault(res.data))
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
        try {
            const res = await api.get('api/vaults/' + vaultId)
            if (res.data == null){
                throw new Error('No vault found with that Id')
            } 
            AppState.activeVault = res.data
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

    async deleteVault(vaultId) {
        await api.delete('api/vaults/' + vaultId)
        AppState.accountVaults = AppState.accountVaults.filter(v => v.id != vaultId)
    }
}
export const vaultsService = new VaultsService()