<template>
    <div class="modal fade" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                <slot></slot>
            </div>
        </div>
    </div>
</template>

<script>
export default {
    setup() {
        return {}
    }
}
</script>