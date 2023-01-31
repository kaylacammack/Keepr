<template>
    <div class="container-fluid">
        <div class="keepImg rounded elevation-3">
            <img class="img-fluid" :src="keep.img" type="button" @click="setActiveKeep(keep.id)">
            <h2 class="text-light ps-2">{{ keep.name }}</h2>
            <router-link :to="{ name: 'Profile', params: { profileId: keep.creatorId } }">
                <img class="creatorPicture img-fluid" :src="keep.creator.picture" :title="keep.creator.name">
            </router-link>
        </div>
    </div>
</template>

<script>
import { Keep } from "../models/Keep";
import { keepsService } from "../services/KeepsService";
import { logger } from "../utils/Logger";
import Pop from "../utils/Pop";
import { ref } from 'vue'
import { Modal } from "bootstrap";
import KeepModal from "./KeepModal.vue";

export default {
    props: {
        keep: { type: Object, required: true },
        user: { type: Object, required: false }
    },
    setup(props) {

        return {
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
            async getKeepById(keepId) {
                try {
                    await keepsService.getKeepById(keepId)
                } catch (error) {
                    logger.error(error)
                    Pop.error(error.message)
                }
            }
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
    right: 0px;
    bottom: 0px;
}

.keepName {
    position: absolute;
    bottom: 0;
    left: 0;
}
</style>