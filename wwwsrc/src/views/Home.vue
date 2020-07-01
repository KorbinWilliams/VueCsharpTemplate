<template></template>

<script>
export default {
  name: "home",
  mounted() {},
  data() {
    return {};
  },
  computed: {},
  methods: {
    logout() {
      this.$store.dispatch("logout");
    }
  },
  components: {}
};
</script>
<style>
</style>