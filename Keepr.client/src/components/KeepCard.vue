<template>
    <div class="card text-bg-dark">
        <img :src="keep.img" class="card-img keepImg" data-bs-toggle="modal" data-bs-target="keepModal">
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
import { useRoute } from "vue-router";
import { Keep } from "../models/Keep";
import { keepsService } from "../services/KeepsService";
import { logger } from "../utils/Logger";
import Pop from "../utils/Pop";

export default {
    props: {
        keep: { type: Keep, required: true },
        user: { type: Object, required: false }
    },
    setup() {
        const route = useRoute()
        async function getKeepById(keepId) {
            try {
                await keepsService.getKeepById(route.params.keepId)
            } catch (error) {
                logger.error(error)
                Pop.error(error.message)
            }
        }
        return {

        }
    }
}
</script>

<style scoped lang="scss">
.keepImg {
    height: 25vh;
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