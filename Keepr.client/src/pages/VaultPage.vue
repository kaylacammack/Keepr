<template>
    <div class="container-fluid" v-if="vaultKeeps">
        <div class="row">
            <img :src="vaultKeeps.img" class="vaultImg">
        </div>
        <h4 class="text-center">
            {{ vaultKeeps.length }} Keeps
        </h4>
        <div v-for="k in vaultKeeps" class="col-12 col-md-3 mb-3 p-4">
            <KeepCard :keep="k" />
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

export default {
    setup() {
        const route = useRoute();
        async function GetKeepsByVaultId() {
            try {
                await vaultsService.GetKeepsByVaultId(route.params.vaultId);
            } catch (error) {
                logger.error(error)
                Pop.error(error.message)
            }
        }
        onMounted(() => {
            GetKeepsByVaultId();
        })
        return {
            vaultKeeps: computed(() => AppState.vaultKeeps),
        }
    }
}
</script>

<style scoped lang="scss">
.vaultImg {
    height: 25vh;
}
</style>