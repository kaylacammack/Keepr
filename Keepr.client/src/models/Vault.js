export class Vault {
    constructor(data) {
        this.creator = data.creator
        this.creatorId = data.creatorId
        this.description = data.description
        this.id = data.id
        this.img = data.img
        this.isPrivate = data.isPrivate
        this.name = data.name
    }
}