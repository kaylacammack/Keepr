<template>
    <router-link :to="{ name: 'Vault', params: { vaultId: vault.id } }">
        <div class="card text-bg-dark">
            <img :src="vault.img" class="card-img vaultImg">
            <div class="card-img-overlay">
                <h5 class="card-title vaultName">{{ vault.name }}</h5>
            </div>
            <div v-if="user?.id == vault.creatorId" type="button" @click="deleteVault(vault.id)"
                class="mdi mdi-key-remove trash" title="Delete Vault"></div>
        </div>
    </router-link>
</template>

<script>
import { AppState } from "../AppState";
import { vaultsService } from "../services/VaultsService";
import { logger } from "../utils/Logger";
import Pop from "../utils/Pop";
import { computed } from "vue";
import { useRoute } from "vue-router";
import { router } from "../router";

export default {
    props: {
        vault: { type: Object, required: true }
    },
    setup() {
        const route = useRoute();
        return {
            user: computed(() => AppState.user),
            async deleteVault(vaultId) {
                try {
                    if (await Pop.confirm()) {
                        await vaultsService.deleteVault(vaultId)
                        Pop.toast("Vault successfully deleted", "success")
                        router.push({ name: "Account" })
                    }
                } catch (error) {
                    logger.error(error)
                    Pop.error(error.message)
                }
            }
        }
    }
}
</script>

<style scoped lang="scss">
.vaultImg {
    height: 25vh;
}

.vaultName {
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
</style>