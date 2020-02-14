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
      <button type="button" class="list-group-item list-group-item-action active">Active item</button>
      <button type="button" class="list-group-item list-group-item-action">Item</button>
      <button type="button" class="list-group-item list-group-item-action">Delete</button>
    </div>
  </div>
</template>

<script>
export default {
  name: "Keep",
  props: ["keepData"]
};
</script>

<style>
.keep-text {
  color: whitesmoke;
}
</style>