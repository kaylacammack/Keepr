<template>
    <div class="modal fade" id="editAccountForm" tabindex="-1" aria-labelledby="editAccountFormLabel"
        aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h1 class="modal-title fs-5" id="editAccountFormLabel">Modal title</h1>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>

                <form @submit.prevent="editAccount()">

                    <div class="form-floating mb-3">
                        <input v-model="editable.name" type="text" class="form-control" id="accountName"
                            placeholder="Name">
                        <label for="accountName">Account Name</label>
                    </div>

                    <div class="form-floating mb-3">
                        <input v-model="editable.coverImg" type="url" class="form-control" id="accountCoverImg"
                            placeholder="Cover Image">
                        <label for="accountCoverImg">Cover Image</label>
                    </div>

                    <div class="form-floating mb-3">
                        <input v-model="editable.picture" type="url" class="form-control" id="accountPicture"
                            placeholder="User Picture">
                        <label for="User Picture">User Picture</label>
                    </div>

                    <button type="submit" class="btn btn-primary">Save changes</button>
                </form>
            </div>
        </div>
    </div>
</template>

<script>
import { ref } from "vue";
import { accountService } from "../services/AccountService";
import { logger } from "../utils/Logger";
import Pop from "../utils/Pop";

export default {
    setup() {
        const editable = ref({})
        return {
            editable,
            async editAccount() {
                try {
                    await accountService.editAccount(editable.value)
                } catch (error) {
                    logger.error(error)
                    Pop.error(error.message)
                }
            }
        }
    }
}
</script>

<style lang="scss" scoped>

</style>