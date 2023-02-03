<template>
    <section class="container-fluid">
        <div class="row">
            <div v-for="k in keeps" class="col-12 col-md-3 mb-3 p-4 text-shadow">
                <KeepCard :keep="k" />
            </div>
        </div>
    </section>
    <KeepModal />

</template>

<script>
import { logger } from "../utils/Logger";
import Pop from "../utils/Pop";
import { keepsService } from "../services/KeepsService.js";
import { onMounted, computed } from "vue";
import { AppState } from "../AppState.js";
import KeepModal from "../components/KeepModal.vue";

export default {
    setup() {
        async function getAllKeeps() {
            try {
                await keepsService.getAllKeeps();
            }
            catch (error) {
                Pop.error(error.message);
                logger.error(error);
            }
        }

        onMounted(() => {
            getAllKeeps();
        });
        return {
            account: computed(() => AppState.account),
            keeps: computed(() => AppState.keeps),
            user: computed(() => AppState.user),
        };
    },
    components: { KeepModal }
}
</script>

<style scoped lang="scss">
.text-shadow {
    text-shadow: 2px 2px 4px #363636;
}
</style>
