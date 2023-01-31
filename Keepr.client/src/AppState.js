import { reactive } from 'vue'

// NOTE AppState is a reactive object to contain app level data
export const AppState = reactive({
  user: {},
  /** @type {import('./models/Account.js').Account} */
  account: {},
  /** @type {import('./models/Keep.js').Keep[]} */
  keeps: [],
    activeKeep: {},
  profile: null,
    /** @type {import('./models/Vault.js').Vault[]} */
    vaults: [],
  profileVaults: [],
  accountVaults: [],
  activeVault: {},
  profileKeeps: [],
  vaultKeeps: null,
  
})
