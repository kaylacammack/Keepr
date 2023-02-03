<template>
    <div class="container-fluid text-shadow">
        <!-- SECTION Profile Information -->
        <div v-if="profile" class="row">
            <!-- TODO Center and have img stretch across 10 columns -->
            <div class="col-10" v-if="profile.coverImg">
                <img :src="profile.coverImg" class="cover-img">
            </div>
            <div v-else class="col-10 alt-cover-img">

            </div>
            <!-- TODO fix profile picture to be centered and partially covering coverImg -->
            <div class="col-10">
                <img :src="profile.picture" :alt="profile.name" class="img-fluid profile-picture rounded-circle">
            </div>
            <div class="col-10">
                <h1 class="text-center">{{ profile.name }}</h1>
                <h4 class="text-center">Vaults: {{ profileVaults.length }} || Keeps: {{ profileKeeps.length }}</h4>
            </div>
        </div>
        <!-- SECTION Profile Vaults -->
        <div class="row">
            <h1 class="text-center">Vaults</h1>
            <div v-for="v in profileVaults" class="col-12 col-md-3 mb-3 p-4">
                <VaultCard :vault="v" />
            </div>
        </div>
        <!-- SECTION Profile Keeps -->
        <div class="row">
            <h1 class="text-center">Keeps</h1>
            <div v-for="k in profileKeeps" class="col-12 col-md-3 mb-3 p-4">
                <ProfileKeepCard :keep="k" />
            </div>
        </div>
    </div>
    <KeepModal />
</template>

<script>
import { onMounted, computed } from "vue";
import { useRoute } from "vue-router";
import { AppState } from "../AppState";
import KeepModal from "../components/KeepModal.vue";
import ProfileKeepCard from "../components/ProfileKeepCard.vue";
import { keepsService } from "../services/KeepsService";
import { profileService } from "../services/ProfileService";
import { vaultsService } from "../services/VaultsService.js";
import { logger } from "../utils/Logger";
import Pop from "../utils/Pop";

export default {
    setup() {
        const route = useRoute();
        async function getProfileById() {
            try {
                await profileService.getProfileById(route.params.profileId);
            }
            catch (error) {
                logger.error(error);
                Pop.error(error.message);
            }
        }
        async function getAllProfileVaults() {
            try {
                await vaultsService.getAllProfileVaults(route.params.profileId);
            }
            catch (error) {
                logger.error(error);
                Pop.error(error.message);
            }
        }
        async function getAllProfileKeeps() {
            try {
                await keepsService.getAllProfileKeeps(route.params.profileId);
            }
            catch (error) {
                logger.error(error);
                Pop.error(error.message);
            }
        }
        onMounted(() => {
            getProfileById();
            getAllProfileVaults();
            getAllProfileKeeps();
        });
        return {
            profile: computed(() => AppState.profile),
            user: computed(() => AppState.user),
            profileVaults: computed(() => AppState.profileVaults),
            profileKeeps: computed(() => AppState.profileKeeps),
        };
    },
    components: { ProfileKeepCard, KeepModal }
}
</script>

<style scoped lang="scss">
.cover-img {
    max-height: 30vh;
    background-size: cover;
    background-position: center;
}

.profile-picture {
    height: 100px;
    width: 100px;
}

.alt-cover-img {
    background-image: url('https: //images.unsplash.com/photo-1502134249126-9f3755a50d78?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1170&q=80');
    background-position: center;
    background-size: cover;
    height: 30vh;
}

.text-shadow {
    text-shadow: 2px 2px 4px #363636;
}
</style>