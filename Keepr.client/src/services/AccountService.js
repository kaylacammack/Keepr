import { AppState } from '../AppState'
import { Account } from "../models/Account"
import { logger } from '../utils/Logger'
import Pop from "../utils/Pop"
import { api } from './AxiosService'

class AccountService {
  async getAccount() {
    try {
      const res = await api.get('/account')
      logger.log(res.data)
      AppState.account = res.data
    } catch (err) {
      logger.error('HAVE YOU STARTED YOUR SERVER YET???', err)
    }
  }

  async getAllAccountVaults() {
    try {
        const res = await api.get('/account/vaults')
        AppState.accountVaults = res.data
    } catch (error) {
        logger.error(error)
        Pop.error(error.message)
    }
  }

  async editAccount(body) {
    const res = await api.put('/account', body)
    AppState.account = new Account(res.data)
  }

}

export const accountService = new AccountService()
