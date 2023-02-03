<template>
    <div class="container-fluid text-shadow">
        <!-- SECTION Account Information -->
        <div v-if="account" class="row">
            <div class="col-10">
                <img :src="account.coverImg" class="cover-img">
            </div>

            <div class="col-10">
                <img :src="account.picture" :alt="account.name" class="img-fluid profile-picture rounded-circle">
            </div>
            <!-- SECTION edit account modal form -->
            <button type="button" class="btn btn-primary col-3" data-bs-toggle="modal"
                data-bs-target="#editAccountForm">
                Edit Account
            </button>

            <EditAccountForm />

            <div class="col-10">
                <h1 class="text-center">{{ account.name }}</h1>
                <h4 class="text-center">Vaults: {{ accountVaults.length }} || Keeps: {{ keeps.length }}</h4>
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
            <div v-for="k in keeps" class="col-12 col-md-3 mb-3 p-4">
                <ProfileKeepCard :keep="k" />
            </div>
        </div>
    </div>
</template>

<script>
import { computed, onMounted } from 'vue'
import { AppState } from '../AppState'
import EditAccountForm from "../components/EditAccountForm.vue";
import ProfileKeepCard from "../components/ProfileKeepCard.vue";
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
            keeps: computed(() => AppState.profileKeeps),
        };
    },
    components: { VaultCard, EditAccountForm, ProfileKeepCard }
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

.text-shadow {
    text-shadow: 2px 2px 4px #363636;
}
</style>
