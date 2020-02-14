<template>
  <div class="dashboard container-fluid main">
    <div class="row">
      <div class="col-12 d-flex justify-content-center">
        <h1 class="text-white">{{this.$auth.userInfo.nickname}}'s Dashboard</h1>
      </div>
    </div>
    <div class="row">
      <div class="col-2 vaults">
        <!-- vaults here on click display keeps in next col-->
        <!-- might put button here for v-show myKeeps vs. vaultKeeps -->
        <div>
          <div v-for="vault in vaults" :key="vault._id">
            <br />
            <p
              class="btn vaultBtn"
              @click="setActiveVault(vault)"
            >{{vault.name}}: {{vault.description}}</p>
            <br />
            <button class="btn btn-danger">Delete {{vault.name}}</button>
          </div>
        </div>
        <div class="form-group">
          <label for="vault-group">Create a vault</label>
          <input type="text" v-model="newVault.name" class="form-control" placeholder="Name" />
          <input
            type="text"
            v-model="newVault.description"
            class="form-control"
            placeholder="Description"
          />
          <button @click="createVault">Submit</button>
        </div>
      </div>
      <div class="col-2">
        <!-- FIXME this col is a temporary offset -->
      </div>
      <div class="col-8">
        <div class="row">
          <div class="col">
            <!-- active keepS AND OR myKeeps -->
            <keep v-for="activeKeep in activeKeeps" :key="activeKeep._id" />
          </div>
        </div>
        <div class="row">
          <div class="col">
            <!-- active keep -->
          </div>
        </div>
      </div>
    </div>
    <!-- public {{ publicKeeps }} user {{ userKeeps }} -->
  </div>
</template>

<script>
import Keep from "../components/keep";

export default {
  // name: "dashboard",
  mounted() {
    this.$store.dispatch("get", {
      address: "vaults",
      commit: "setItem",
      commitAddress: "vaults"
    });
  },
  data() {
    return {
      newVault: {
        name: "",
        description: ""
      }
    };
  },
  methods: {
    createVault() {
      this.$store.dispatch("create", {
        data: this.newVault,
        address: "vaults",
        commitAddress: "vaults",
        commit: "addItem"
      });
    },
    setActiveVault(vault) {
      this.$store
        .dispatch("setActive", {
          data: vault,
          commitAddress: "activeVault",
          commit: "setItem"
        })
        .then(res =>
          this.$store.dispatch("getOneByAnother", {
            address1: "vaultkeeps",
            address2: "keeps",
            data: this.$store.state.activeVault,
            commit: "setItem",
            commitAddress: "activeKeeps"
          })
        );
    }
  },
  computed: {
    vaults() {
      return this.$store.state.vaults;
    },
    activeKeeps() {
      return this.$store.state.activeKeeps;
    }
  },
  components: {
    Keep
  }
};
</script>

<style scoped>
.main {
  background-color: #011640;
}
.vaults {
  background-color: #3e7c71;
}
.vaultBtn {
  border-radius: 2px;
  border-color: black;
  text-shadow: 1px 1px red;
}
.vaultBtn:hover {
  border-radius: 2px;
  border-color: black;
  text-shadow: 1px 1px rgb(38, 0, 255);
}
</style>
