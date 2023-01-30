<template>
    <!-- SECTION Home -->
    <nav class="navbar navbar-expand-lg navbar-dark px-3 d-flex">
        <router-link class="navbar-brand d-flex" :to="{ name: 'Home' }">
            <button class="btn selectable">Home</button>
        </router-link>

        <!-- SECTION Create Vault -->
        <button data-bs-target="#create-vault" data-bs-toggle="modal">Create Vault</button>

        <ModalComponent id="create-vault">
            <CreateVaultForm />
        </ModalComponent>

        <!-- SECTION Login -->
        <div class="collapse navbar-collapse" id="navbarText">
            <Login />
        </div>
    </nav>
</template>

<script>
import Login from './Login.vue'
import ModalComponent from "./ModalComponent.vue";
export default {
    setup() {
        return {}
    },
    components: { Login, ModalComponent }
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
