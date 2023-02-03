<template>
    <div class="card text-bg-dark">
        <img :src="keep.img" class="card-img keepImg" type="button" @click="setActiveKeep(keep.keepId)">
        <div class="card-img-overlay">
            <h5 class="card-title keepName ms-2">{{ keep.name }}</h5>
        </div>
        <div v-if="user?.id == vault.creatorId" type="button" @click="RemoveKeepFromVault(keep.vaultKeepId)"
            class="mdi mdi-key-remove mdi-36px trash" title="Remove Keep From Vault"></div>
    </div>
</template>

<script>
import { AppState } from "../AppState";
import { computed } from "vue";
import { logger } from "../utils/Logger";
import Pop from "../utils/Pop";
import { vaultKeepsService } from "../services/VaultKeepService";

export default {
    props: {
        keep: { type: Object, required: true },
    },
    setup() {
        return {
            vault: computed(() => AppState.activeVault),
            user: computed(() => AppState.user),
            async RemoveKeepFromVault(vaultKeepId) {
                try {
                    await vaultKeepsService.RemoveKeepFromVault(vaultKeepId)
                    Pop.toast("Keep successfully removed from vault", "success")
                } catch (error) {
                    logger.error(error);
                    Pop.error(error.message);
                }
            }
        }
    }
}
</script>

<style scoped lang="scss">
.keepImg {
    position: relative;
    width: 100%;
}

.card-img-overlay {
    pointer-events: none;
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
</style>