<template>
    <div class="modal fade" id="createVault" tabindex="-1" aria-labelledby="createVaultLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h1 class="modal-title fs-5" id="createVaultLabel">Create Vault</h1>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>

                <form @submit.prevent="createVault">

                    <div class="form-floating mb-3">
                        <input v-model="editable.name" required type="text" class="form-control" id="vaultName"
                            placeholder="Vault Name">
                        <label for="vaultName">Vault Name</label>
                    </div>

                    <div class="form-floating mb-3">
                        <input v-model="editable.img" required type="url" class="form-control" id="vaultImg"
                            placeholder="Vault Image">
                        <label for="vaultImg">Vault Image</label>
                    </div>

                    <div class="form-floating mb-3">
                        <input v-model="editable.description" required type="text" class="form-control"
                            id="vaultDescription" placeholder="Description">
                        <label for="vaultDescription">Vault Description</label>
                    </div>

                    <div class="form-check">
                        <input v-model="editable.isPrivate" type="checkbox" class="form-check-input" value="false"
                            id="isPrivate">
                        <label class="form-check-label" for="isPrivate"> Make Vault Private?</label>
                    </div>

                    <button type="submit" class="btn btn-primary">Create Vault</button>
                </form>
            </div>
        </div>
    </div>

</template>

<script>
import { ref } from 'vue';
import { vaultsService } from "../services/VaultsService";
import { logger } from "../utils/Logger";
import Pop from "../utils/Pop";
import { Modal } from 'bootstrap';
export default {
    setup() {
        const editable = ref({})
        return {
            editable,
            async createVault() {
                try {
                    logger.log(editable.value)
                    await vaultsService.createVault(editable.value)
                    logger.log(editable.value)
                    Pop.toast('Vault Created', 'success')
                    editable.value = {}
                    Modal.getOrCreateInstance('#createVault').hide()
                } catch (error) {
                    logger.error(error)
                    Pop.error(error.message)
                }
            }
        }
    }
}
</script>