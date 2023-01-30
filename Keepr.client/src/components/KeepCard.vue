<template>
    <div class="card text-bg-dark">
        <img :src="keep.img" class="card-img keepImg">
        <div class="card-img-overlay">
            <h5 class="card-title keepName ms-2">{{ keep.name }}</h5>

            <router-link :to="{ name: 'Profile', params: { profileId: keep.creatorId } }">
                <img :src="keep.creator.picture" class="rounded-pill creatorPicture mb-2 me-2"
                    :title="keep.creator.name">
            </router-link>

        </div>
    </div>
</template>

<script>
export default {
    props: {
        keep: { type: Object, required: true }
    },
    setup() {
        return {}
    }
}
</script>

<style scoped lang="scss">
.keepImg {
    height: 25vh;
}

.creatorPicture {
    height: 50px;
    width: 50px;
    position: absolute;
    right: 0px;
    bottom: 0px;
}

.keepName {
    position: absolute;
    bottom: 0;
    left: 0;
}
</style>