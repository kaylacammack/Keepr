import { logger } from "../utils/Logger.js"
import Pop from "../utils/Pop.js"
import { api } from "./AxiosService.js"
import { AppState } from "../AppState.js"

class KeepsService {
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
}

export const keepsService = new KeepsService()