<template>
    <div class="card text-bg-dark">
        <!-- FIXME debug this component and get delete working -->
        <img :src="keep.img" class="card-img keepImg" type="button" @click="setActiveKeep(keep.keepId)">
        <div class="card-img-overlay">
            <h5 class="card-title keepName ms-2">{{ keep.name }}</h5>

        </div>
        <div v-if="user?.id == keep.creatorId" type="button" @click="deleteKeep(keep.keepId)"
            class="mdi mdi-delete-outline mdi-36px trash" title="Delete Keep">
        </div>
    </div>
</template>



<script>
import { AppState } from "../AppState";
import { keepsService } from "../services/KeepsService";
import { logger } from "../utils/Logger";
import Pop from "../utils/Pop";
import { Modal } from "bootstrap";
import { computed } from "vue";
import KeepModal from "./KeepModal.vue";



export default {
    props: {
        keep: { type: Object, required: true },
        user: { type: Object, required: false }
    },
    setup() {
        return {
            account: computed(() => AppState.account),
            user: computed(() => AppState.user),
            async setActiveKeep(keepId) {
                try {
                    await keepsService.setActiveKeep(keepId);
                    Modal.getOrCreateInstance(document.getElementById("keep-modal")).toggle();
                }
                catch (error) {
                    logger.error(error);
                    Pop.error(error.message);
                }
            },
            async deleteKeep(keepId) {
                try {
                    await keepsService.deleteKeep(keepId)
                    Pop.toast("Keep successfully deleted", "success")

                } catch (error) {
                    logger.error(error)
                    Pop.error(error.message)
                }
            }
        };
    },
    components: { KeepModal }
}
</script>

<style scoped lang="scss">
.keepImg {
    position: relative;
    width: 100%;
}

.creatorPicture {
    height: 50px;
    width: 50px;
    position: absolute;
    right: 16px;
    bottom: 8px;
}

.keepName {
    position: absolute;
    bottom: 0;
    left: 0;
}

.trash {
    color: red;
    position: absolute;
    bottom: 2%;
    right: 1%;
}

.card-img-overlay {
    pointer-events: none;
}
</style>