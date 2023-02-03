<template>
    <div class="container-fluid" v-if="vaultKeeps">
        <div class="row bg-image align-items-center">
            <div class="col-10">
                <h1 class="ms-4 text-center text-light">
                    {{ vault.name }}
                </h1>
            </div>
        </div>
        <h4 class="text-center">
            {{ vaultKeeps.length }} Keeps
        </h4>
        <div class="row">
            <div v-for="k in vaultKeeps" class="col-12 col-md-3 mb-3 p-4">
                <ProfileKeepCard :keep="k" />
            </div>
        </div>
    </div>
</template>

<script>
import { onMounted, computed } from "vue";
import { useRoute } from "vue-router";
import { vaultsService } from "../services/VaultsService";
import { logger } from "../utils/Logger";
import Pop from "../utils/Pop";
import { AppState } from "../AppState";
import ProfileKeepCard from "../components/ProfileKeepCard.vue";

export default {
    setup() {
        const route = useRoute();
        async function getVaultById() {
            try {
                await vaultsService.getVaultById(route.params.vaultId)
            } catch (error) {
                logger.error(error)
                Pop.error(error.message)
                // FIXME if there is an error
                // you might need to send them away...
            }
        }
        async function GetKeepsByVaultId() {
            try {
                await vaultsService.GetKeepsByVaultId(route.params.vaultId);
            }
            catch (error) {
                logger.error(error);
                Pop.error(error.message);
            }
        }
        onMounted(() => {
            getVaultById();
            GetKeepsByVaultId();
        });
        return {
            vaultKeeps: computed(() => AppState.vaultKeeps),
            vault: computed(() => AppState.activeVault),
            vaultImg: computed(() => `url(${AppState.activeVault.img})`)

        };
    },
    components: { ProfileKeepCard }
}
</script>

<style scoped lang="scss">
.vaultImg {
    height: 25vh;
}

.bg-image {
    min-height: 30vh;
    background-image: v-bind(vaultImg);
    background-size: cover;
    background-position: center;
}
</style>