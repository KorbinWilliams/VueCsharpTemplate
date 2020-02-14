<template>
  <div class="keep keep-text">
    <h3>{{keepData.name}}</h3>
    <img type="button" :src="keepData.img" alt="keep image" />
    <h5>{{keepData.description}}</h5>
    <p>
      -
      <span class="text-danger">Views: {{keepData.views}}</span>
      -
      <span class="text-warning">Keeps: {{keepData.keeps}}</span>
      -
      <span class="text-primary">Shares: {{keepData.shares}}</span>
    </p>
    <div class="list-group">
      <button type="button" class="list-group-item list-group-item-action">Move to vault</button>
      <button type="button" class="list-group-item list-group-item-action">Item</button>
      <button
        @mouseover="setActiveKeep()"
        type="button"
        class="list-group-item list-group-item-action"
        @click="deleteKeep"
      >Delete</button>
    </div>
  </div>
</template>

<script>
export default {
  name: "Keep",
  props: ["keepData"],
  data() {
    return {
      inActiveKeep: false
    };
  },
  methods: {
    setActiveKeep() {
      this.$store.dispatch("setActive", {
        data: this.keepData,
        commitAddress: "activeKeep",
        commit: "setItem"
      });
    },
    deleteKeep() {
      let keep = this.$store.state.activeKeep;
      console.log(keep);
      // this.inActiveCheck();
      if (this.inActiveKeep == false) {
        this.$store.dispatch("delete", {
          address: "keeps",
          data: keep,
          commit: "removeItem",
          commitAddress: "publicKeeps"
        });
      }
    }
  }
};
</script>

<style>
.keep-text {
  color: whitesmoke;
}
</style>