import { logger } from "../utils/Logger.js"
import Pop from "../utils/Pop.js"
import { api } from "./AxiosService.js"
import { AppState } from "../AppState.js"
import { Keep } from "../models/Keep.js"

class KeepsService {
    async createKeep(keepData) {
        try {
            const res = await api.post('api/keeps', keepData)
            AppState.keeps.push(new Keep(res.data))
        } catch (error) {
            Pop.error(error.message)
            logger.error(error)
        }
    }
    async getAllKeeps() {
        try {
            const res = await api.get('api/keeps')
            logger.log('[Getting all keeps]', res.data)
            AppState.keeps = res.data
        } catch (error) {
            Pop.error(error.message)
            logger.error(error)
        }
    }

    async getAllProfileKeeps(profileId) {
        const res = await api.get('api/profiles/' + profileId + '/keeps')
        AppState.profileKeeps = res.data.map(k => new Keep(k))
    }

    async setActiveKeep(keepId) {
        AppState.activeKeep = {}
        const keep = await this.getKeepById(keepId)
        AppState.activeKeep = keep
    }

    async getKeepById(keepId) {
        const res = await api.get('api/keeps/' + keepId)
        return res.data
    }

    async deleteKeep(keepId) {
            const res = await api.delete('api/keeps/' + keepId)
            logger.log(res)
            AppState.keeps = AppState.keeps.filter(k => k.keepId != keepId)
            AppState.profileKeeps = AppState.profileKeeps.filter(k => k.keepId != keepId)
        }
    }


export const keepsService = new KeepsService()