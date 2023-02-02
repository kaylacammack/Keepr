<template>
    <div class="modal fade" id="vaultKeepModal" tabindex="-1" aria-labelledby="vaultKeepModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header d-block">
                    <div class="row" v-if="user">
                        <!-- SECTION keep img -->
                        <img class="col-12 col-md-6 image-fluid draggable-none user-select-none" :src="keep.img"
                            alt="Keep Image">
                        <!-- SECTION keep details -->
                        <div class="col-12 col-md-6">
                            <div class="row mt-0 me-3">
                                <h5 class="col-2 offset-2 align-self-end"><i class="mdi mdi-eye"></i>{{ keep.views }}
                                </h5>
                                <h5 class="col-2 align-self-end"><i class="mdi mdi-lock"></i>{{ keep.kept }}</h5>
                                <span id="modal-close" type="button" title="close-button"
                                    class="mdi mdi-close mdi-36px col-2 offset-2 mt-0 pt-0"
                                    data-bs-dismiss="modal"></span>
                            </div>
                            <h1 class="modal-title text-center" id="vaultKeepModalLabel">{{ keep.name }}</h1>
                            <div class="modal-body">
                                <p>{{ keep.description }}</p>
                            </div>
                            <!-- SECTION remove keep from vault -->
                            <div v-if="user?.id == keep.creatorId" type="button"
                                @click="removeKeepFromVault(keep.vaultKeepId)" class="mdi mdi-lock-remove mdi-36px"
                                title="Remove Keep From Vault"></div>
                        </div>
                    </div>
                </div>

            </div>
        </div>
    </div>
</template>

<script>
import { AppState } from "../AppState";
import { computed } from "vue";
import { logger } from "../utils/Logger";
import Pop from "../utils/Pop";
import { vaultKeepsService } from "../services/VaultKeepService";

export default {
    setup() {
        return {
            keep: computed(() => AppState.activeKeep),
            user: computed(() => AppState.user),
            account: computed(() => AppState.account),
            async removeKeepFromVault(vaultKeepId) {
                try {
                    await vaultKeepsService.removeKeepFromVault(vaultKeepId)
                    Pop.toast("Keep successfully removed from vault", "success")
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