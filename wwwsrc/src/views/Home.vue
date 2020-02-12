<template>
  <div class="home container-fluid">
    <div class="row">
      <div class="col text-center">
        <h1>Home</h1>
      </div>
    </div>
    <div class="row">
      <div class="col-3">
        <p>This is where the filters will go</p>
      </div>
      <div class="col-9">
        <div class="row">
          <keep class="col-3" v-for="keep in keeps" :key="keep.id" :keepData="keep" />
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import Keep from "../components/keep";

export default {
  name: "home",
  mounted() {
    this.$store.dispatch("get", {
      commit: "setItem",
      address: "keeps",
      commitAddress: "publicKeeps"
    });
  },
  computed: {
    user() {
      return this.$store.state.user;
    },
    keeps() {
      return this.$store.state.publicKeeps;
    }
  },
  methods: {
    logout() {
      this.$store.dispatch("logout");
    }
  },
  components: {
    Keep
  }
};
</script>
