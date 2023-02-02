import { AppState } from "../AppState"
import { VaultKeep } from "../models/VaultKeep"
import Pop from "../utils/Pop"
import { api } from "./AxiosService"
import { keepsService } from "./KeepsService"
import { vaultsService } from "./VaultsService"

class VaultKeepsService {
    async addKeepToVault(vaultId, keepId) {
        const vaultKeep = {}
        await vaultsService.getVaultById(vaultId)
        await keepsService.getKeepById(keepId)
        vaultKeep.keepId = keepId
        vaultKeep.vaultId = vaultId
        const res = await api.post('api/vaultkeeps', vaultKeep)
        AppState.vaultKeeps = new VaultKeep(res.data)
    }
}
export const vaultKeepsService = new VaultKeepsService()