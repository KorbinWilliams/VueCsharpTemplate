import Vue from "vue";
import Vuex from "vuex";
import Axios from "axios";
import router from "./router";

Vue.use(Vuex);

let baseUrl = location.host.includes("localhost") ? "https://localhost:5001/" : "/";

let api = Axios.create({
  baseURL: baseUrl + "api/",
  timeout: 3000,
  withCredentials: true
});

export default new Vuex.Store({
  state: {
    publicKeeps: [],
    // for viewing your vaults
    activeKeeps: [],
    vaults: [],
    activeVault: {}
    // do I need vaultkeeps??? I don't think so. Screw sql
  },
  mutations: {
    addItem(state, payload) {
      state[payload.address].push(payload.data);
    },
    setItem(state, payload) {
      state[payload.address] = payload.data;
    },
    removeItem(state, payload) {
      state[payload.address].filter(item => item._id = payload.data._id)
    },
    resetState(state) {
      state = {
        publicKeeps: [],
        activeKeeps: [],
        vaults: [],
        activeVault: {}
      }
    }
  },
  actions: {
    setBearer({ }, bearer) {
      api.defaults.headers.authorization = bearer;
    },
    resetBearer() {
      api.defaults.headers.authorization = "";
    },
    // payload.address = url
    // payload.commit = mutation
    // data = data usually res.data
    // payload.commitAddress = store.state. <----
    get({ commit }, payload) {
      api
        .get("" + payload.address)
        .then(res => {
          commit(payload.commit, {
            data: res.data,
            address: payload.commitAddress
          });
        })
        .catch(e => console.error(e));
    },
    getOne({ commit }, payload) {
      api
        .get("" + payload.address + "/" + payload.data._id)
        .then(res => {
          commit(payload.commit, {
            data: res.data,
            address: payload.commitAddress
          });
        })
        .catch(e => console.error(e));
    },
    // for using ref's. address 1 is where the id/ref comes from, address 2 is what youre looking for, commitAddress is where it's going in the state.
    getOneByAnother({ commit }, payload) {
      api
        .get(
          "" +
          payload.address1 +
          "/" +
          payload.data._id +
          "/" +
          payload.address2
        )
        .then(res => {
          commit(payload.commit, {
            data: res.data,
            address: payload.commitAddress
          });
        });
    },
    create({ commit }, payload) {
      api
        .post("" + payload.address, payload.data)
        .then(res => {
          commit(payload.commit, {
            data: res.data,
            address: payload.commitAddress
          });
        })
        .catch(e => console.error(e));
    },
    edit({ commit }, payload) {
      api
        .put(
          "" + payload.address + "/" + payload.data._id || payload._id,
          payload.data
        )
        .then(res => {
          commit(payload.commit, {
            data: res.data,
            address: payload.commitAddress
          });
        })
        .catch(e => console.error(e));
    },
    delete({ commit }, payload) {
      api.delete("" + payload.address + "/" + payload.data._id).then(res => {
        commit(payload.commit, {
          data: res.data,
          address: payload.commitAddress
        });
      });
    },
    setActive({ commit }, payload) {
      commit(payload.commit, {
        data: payload.data,
        address: payload.commitAddress
      })
    }
  }
});
