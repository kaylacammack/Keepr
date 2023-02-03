<template>
    <div class="card text-bg-dark">
        <img :src="keep.img" class="card-img keepImg" type="button" @click="setActiveKeep(keep.id)">
        <div class="card-img-overlay">
            <h5 class="card-title keepName ms-2">{{ keep.name }}</h5>
            <router-link :to="{ name: 'Profile', params: { profileId: keep.creatorId } }">
                <img :src="keep.creator.picture" class="rounded-pill creatorPicture mb-2 me-2"
                    :title="keep.creator.name">
            </router-link>
        </div>
    </div>

</template>

<script>
import { keepsService } from "../services/KeepsService";
import { logger } from "../utils/Logger";
import Pop from "../utils/Pop";
import { computed } from 'vue'
import { Modal } from "bootstrap";
import KeepModal from "./KeepModal.vue";
import { AppState } from '../AppState'


export default {
    props: {
        keep: { type: Object, required: true },
        user: { type: Object, required: false }
    },
    setup() {

        return {
            account: computed(() => AppState.account),
            async setActiveKeep(keepId) {
                try {
                    await keepsService.setActiveKeep(keepId);
                    Modal.getOrCreateInstance(document.getElementById("keep-modal")).toggle();
                }
                catch (error) {
                    logger.error(error)
                    Pop.error(error.message)
                }
            },
        }
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
    pointer-events: fill;
}

.keepName {
    position: absolute;
    bottom: 0;
    left: 0;
}

.card-img-overlay {
    pointer-events: none;
}
</style>