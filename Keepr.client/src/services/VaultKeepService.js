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
        await api.post('api/vaultkeeps', vaultKeep)
        Pop.toast('Keep successfully added to vault', "success")
    }
}
export const vaultKeepsService = new VaultKeepsService()