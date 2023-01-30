<template>
    <form @submit.prevent="createVault()">
        <div class="modal-body">

            <div class="form-floating mb-3">
                <input v-model="editable.name" required type="text" class="form-control" id="vaultName"
                    placeholder="Vault Name">
                <label for="vaultName">Vault Name</label>
            </div>
        </div>

        <div class="form-floating mb-3">
            <input v-model="editable.img" required type="url" class="form-control" id="vaultImg"
                placeholder="Vault Cover Image">
            <label for="vaultImg">Vault Cover Image</label>
        </div>

        <div class="form-floating mb-3">
            <input v-model="editable.description" required type="text" class="form-control" id="vaultDescription"
                placeholder="Description">
            <label for="vaultDescription">Vault Description</label>
        </div>

        <div class="form-check">
            <input class="form-check-input" type="checkbox" value="true" id="isPrivate">
            <label class="form-check-label" for="isPrivate">Private</label>
        </div>

        <div class="modal-footer">
            <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
            <button type="submit" class="btn btn-primary">Save changes</button>
        </div>
    </form>
</template>

<script>
import { ref } from 'vue';
import { vaultsService } from "../services/VaultsService";
import { logger } from "../utils/Logger";
import Pop from "../utils/Pop";
export default {
    setup() {
        const editable = ref({})
        return {
            editable,
            async createVault() {
                try {
                    await vaultsService.createVault(editable.value)
                } catch (error) {
                    logger.error(error)
                    Pop.error(error.message)
                }
            }
        }
    }
}
</script>