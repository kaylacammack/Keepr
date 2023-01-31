<template>
    <div class="container-fluid">
        <!-- SECTION Account Information -->
        <div v-if="account" class="row">
            <!-- FIXME coverImage not displaying -->
            <div class="col-10">
                <img :src="account.coverImg" class="cover-img">
            </div>

            <div class="col-10">
                <img :src="account.picture" :alt="account.name" class="img-fluid profile-picture rounded-circle">
            </div>
            <!-- FIXME Edit account gives a 404 error message, backend not set up to support editing account -->
            <!-- SECTION edit account modal form -->
            <button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#editAccountForm">
                Edit Account
            </button>

            <EditAccountForm />

            <div class="col-10">
                <h1 class="text-center">{{ account.name }}</h1>
                <!-- TODO Add number of vaults & keeps -->
            </div>
        </div>
        <!-- SECTION Account Vaults -->
        <div class="row">
            <h1 class="text-center">Vaults</h1>
            <div v-for="v in accountVaults" class="col-12 col-md-3 mb-3 p-4">
                <VaultCard :vault="v" />
            </div>
        </div>
        <!-- SECTION Account Keeps -->
        <div class="row">
            <h1 class="text-center">Keeps</h1>

        </div>
    </div>
</template>

<script>
import { computed, onMounted } from 'vue'
import { AppState } from '../AppState'
import EditAccountForm from "../components/EditAccountForm.vue";
import VaultCard from "../components/VaultCard.vue";
import { accountService } from "../services/AccountService";
import { logger } from "../utils/Logger";
import Pop from "../utils/Pop";
export default {
    setup() {
        async function getAllAccountVaults() {
            try {
                await accountService.getAllAccountVaults();
            }
            catch (error) {
                logger.error(error);
                Pop.error(error.message);
            }
        }

        onMounted(() => {
            getAllAccountVaults();
        });
        return {
            account: computed(() => AppState.account),
            accountVaults: computed(() => AppState.accountVaults),
        };
    },
    components: { VaultCard, EditAccountForm }
}
</script>

<style scoped>
.cover-img {
    height: 30vh;
    background-size: cover;
    background-position: center;
}

.profile-picture {
    height: 100px;
    width: 100px;
}
</style>
