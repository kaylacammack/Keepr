<template>
    <div class="modal fade modal-xl" id="keep-modal" tabindex="-1" aria-labelledby="keep-modalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header d-block">
                    <div class="row">

                        <!-- SECTION keep img -->
                        <img class="col-12 col-md-6 image-fluid draggable-none user-select-none" :src="keep.img"
                            alt="keep image">

                        <!-- SECTION keep details -->
                        <div class="col-12 col-md-6">
                            <div class="row mt-0 me-3 ">
                                <h5 class="col-2 offset-2 align-self-end"><i class="mdi mdi-eye"></i> {{ keep.views }}
                                </h5>
                                <h5 class="col-2 align-self-end"><i class="mdi mdi-lock"></i> {{ keep.kept }}</h5>
                                <span id="modal-close" type="button" title="close button"
                                    class="mdi mdi-close mdi-36px col-1 offset-2 mt-0 pt-0"
                                    data-bs-dismiss="modal"></span>
                            </div>
                            <h1 class="modal-title text-center" id="keep-modalLabel">{{ keep.name }}</h1>
                            <div class="modal-body">
                                <p>{{ keep.description }}</p>
                            </div>
                            <!-- SECTION Add Keep to Vault -->
                            <!-- TODO Align drop down to bottom left of column -->

                            <div v-if="user" class="col-3 align-self-end dropdown">
                                <h5 class="btn dropdown-toggle" type="button" id="dropdownMenuButton"
                                    title="select a vault" data-bs-toggle="dropdown" aria-expanded="false"><i
                                        class="mdi mdi-plus-thick"></i> <b> to Vault</b></h5>
                                <div class="dropdown-menu vaultList" role="menu" aria-labelledby="dropdownMenuButton">
                                    <div v-for="v in vaults" :key="v.id">
                                        <h5 class="dropdown-item btn" role="menuitem" :title="v"
                                            @click="addKeepToVault(v.id, keep.id)">
                                            {{ v.name }}
                                        </h5>
                                    </div>
                                </div>
                            </div>
                            <div v-if="keep.creatorId">
                                <router-link :to="{ name: 'Profile', params: { profileId: keep.creatorId } }">
                                    <img :src="keep.creator.picture" class="rounded-pill creatorPicture mb-2 me-2"
                                        :title="keep.creator.name" data-bs-dismiss="modal">
                                </router-link>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</template>

<script>
import { computed } from "vue";
import { AppState } from "../AppState.js";
import { keepsService } from "../services/KeepsService";
import { vaultKeepsService } from "../services/VaultKeepService";
import { logger } from "../utils/Logger";
import Pop from "../utils/Pop";

export default {

    setup() {
        return {
            keep: computed(() => AppState.activeKeep),
            user: computed(() => AppState.user),
            account: computed(() => AppState.account),
            vaults: computed(() => AppState.accountVaults),
            async deleteKeep(keepId) {
                try {
                    await keepsService.deleteKeep(keepId)
                    Pop.toast("Keep successfully deleted", "success")
                } catch (error) {
                    logger.error(error)
                    Pop.error(error.message)
                }
                document.getElementById("modal-close").click()
            },
            async addKeepToVault(vaultId, keepId) {
                try {
                    await vaultKeepsService.addKeepToVault(vaultId, keepId)
                    Pop.toast("Keep successfully added to vault", "success")
                } catch (error) {
                    logger.error(error)
                    Pop.error(error.message)
                }
            },
        }
    }
}
</script>

<style lang="scss" scoped>
.keep-modal-profile-img {
    height: 45px;
    width: 45px;
    position: absolute;
    bottom: 3%;
    left: 50%;

    &:hover {
        transform: scale(1.002);
        transition: ease-in-out;
        filter: hue-rotate(60deg);
    }
}

.trash {
    color: red;
    position: absolute;
    bottom: 2%;
    right: 1%;

    &:hover {
        transform: scale(1.01);
        transition: ease-in-out;
        filter: hue-rotate(125deg);
    }
}

.vaultList {
    position: absolute;
    inset: 0px auto auto 0px;
    margin: 0px;
    transform: translate(0px, 40px);
    overflow-y: scroll;
    height: 288px;
}

.creatorPicture {
    height: 50px;
    width: 50px;
    position: absolute;
    right: 16px;
    bottom: 8px;
}
</style>