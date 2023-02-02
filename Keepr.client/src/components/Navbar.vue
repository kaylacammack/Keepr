<template>
    <!-- SECTION Home -->
    <nav class="navbar navbar-expand-lg navbar-dark px-3 d-flex">
        <router-link class="navbar-brand d-flex" :to="{ name: 'Home' }">
            <button class="btn selectable">Home</button>
        </router-link>

        <!-- SECTION Create Vault/Keep Form Modal -->
        <!-- TODO Have drop down button to create vault/keep -->
        <button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#createVault">
            Create Vault
        </button>

        <button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#createKeep">
            Create Keep
        </button>

        <CreateVaultForm />
        <CreateKeepForm />
        <!-- SECTION Login -->
        <!-- TODO Have login display in far right corner and change styling so it's not a light green -->
        <div class="collapse navbar-collapse" id="navbarText">
            <Login />
        </div>
    </nav>
</template>

<script>
import CreateKeepForm from "./CreateKeepForm.vue";
import Login from './Login.vue'
export default {
    setup() {
        return {}
    },
    components: { Login, CreateKeepForm }
}
</script>

<style scoped>
a:hover {
    text-decoration: none;
}

.nav-link {
    text-transform: uppercase;
}

.navbar-nav .router-link-exact-active {
    border-bottom: 2px solid var(--bs-success);
    border-bottom-left-radius: 0;
    border-bottom-right-radius: 0;
}

@media screen and (min-width: 768px) {
    nav {
        height: 64px;
    }
}
</style>
