<template>
    <div class="card text-bg-dark">
        <img :src="vault.img" class="card-img vaultImg">
        <div class="card-img-overlay">
            <h5 class="card-title vaultName">{{ vault.name }}</h5>
        </div>
    </div>
</template>

<script>
import { Vault } from "../models/Vault";

export default {
    props: {
        vault: { type: Vault, required: true }
    },
    setup() {
        return {}
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
</style>