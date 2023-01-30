<template>
    <section class="container-fluid">
        <button type="button" class="btn btn-primary" data-bs-target="#keep-details"
            data-bs-toggle="modal">KeepModal</button>
        <div class="row">
            <div v-for="k in keeps" class="col-12 col-md-3 mb-3 p-4">
                <KeepCard :keep="k" />
            </div>
        </div>
    </section>

    <ModalComponent id="keep-details">
        <KeepModal />
    </ModalComponent>
</template>

<script>
import { logger } from "../utils/Logger";
import Pop from "../utils/Pop";
import { keepsService } from "../services/KeepsService.js";
import { onMounted, computed } from "vue";
import { AppState } from "../AppState.js";

export default {
    setup() {
        async function getAllKeeps() {
            try {
                await keepsService.getAllKeeps();
            } catch (error) {
                Pop.error(error.message)
                logger.error(error)
            }
        }
        onMounted(() => {
            getAllKeeps();
        });
        return {
            keeps: computed(() => AppState.keeps),
        }
    }
}
</script>

<style scoped lang="scss">

</style>
