<template>

</template>

<script>
import { computed } from 'vue'
import { AppState } from '../AppState'
export default {
    setup() {
        return {
            account: computed(() => AppState.account)
        }
    }
}
</script>

<style scoped>

</style>
