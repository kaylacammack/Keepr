<template>
    <div class="modal fade" id="createKeep" tabindex="-1" aria-labelledby="createKeepLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h1 class="modal-title fs-5" id="createKeepLabel">Create Keep</h1>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <form @submit.prevent="createKeep">

                    <div class="form-floating mb-3">
                        <input v-model="editable.name" required type="text" class="form-control" id="keepName"
                            placeholder="Keep Name">
                        <label for="keepName">Keep Name</label>
                    </div>

                    <div class="form-floating mb-3">
                        <input v-model="editable.img" required type="url" class="form-control" id="keepImg"
                            placeholder="Keep Image">
                        <label for="keepImg">Keep Image</label>
                    </div>

                    <div class="form-floating mb-3">
                        <input v-model="editable.description" required type="text" class="form-control"
                            id="vaultDescription" placeholder="Description">
                        <label for="keepDescription">Keep Description</label>
                    </div>


                    <button type="submit" class="btn btn-primary">Create Keep</button>
                </form>
            </div>
        </div>
    </div>
</template>

<script>
import { ref } from "vue";
import { keepsService } from "../services/KeepsService";
import { logger } from "../utils/Logger";
import Pop from "../utils/Pop";
import { Modal } from 'bootstrap';
export default {
    setup() {
        const editable = ref({})
        return {
            editable,
            async createKeep() {
                try {
                    await keepsService.createKeep(editable.value)
                    editable.value = {}
                    Modal.getOrCreateInstance('#createKeep').hide()
                } catch (error) {
                    logger.error(error)
                    Pop.error(error.message)
                }
            }
        }
    }
}
</script>

<style lang="scss" scoped>

</style>