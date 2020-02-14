<template>
  <div class="dashboard container-fluid">
    <div class="row">
      <div class="col justify-content center">
        <h1>{{this.$auth.userInfo.nickname}}'s Dashboard ssss</h1>
      </div>
    </div>
    public {{ publicKeeps }} user {{ userKeeps }}
  </div>
</template>

<script>
export default {
  mounted() {
    this.store.$state.dispatch("get", {
      address: "vaults",
      commit: "setItem",
      commitAddress: "vaults"
    });
  },
  computed: {
    vaults() {
      return this.store.$state.vaults;
    }
  }
};
</script>

<style></style>
