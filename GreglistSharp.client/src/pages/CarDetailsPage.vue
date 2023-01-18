<template>
  <div v-if="car" class="container">
    <!-- viewing car -->
    <div v-if="editMode == false" class="row justify-content-center">
      <div class="col-12 text-center">
        <h1>{{ car.make }} | {{ car.model }}</h1>
        <h5>{{ car.year }}</h5>
      </div>
      <div class="col-6">
        <img class="img-fluid" :src="car.imgUrl" alt="" />
      </div>
      <div class="col-6 d-flex flex-column justify-content-between">
        <p>{{ car.description }}</p>
        <h3 class="bg-success p-2 text-dark rounded-pill">${{ car.price }}</h3>
      </div>
      <button
        v-if="car.creatorId == account.id"
        class="btn btn-info"
        @click="editMode = true"
      >
        edit <i class="mdi mdi-pencil"></i>
      </button>
    </div>
    <!-- editing car -->
    <div v-else class="row">
      <!-- props to pass car data  -->
      <!-- @carEdited is a custom event that happens with CarForm emits `carEdited` -->
      <CarForm :carData="car" @carEdited="editMode = false" />
      <!-- <button class="btn btn-success" @click="editCar">save <i class="mdi mdi-floppy"></i></button> -->
    </div>
  </div>
</template>


<script>
import { AppState } from "../AppState";
import { computed, onMounted, ref } from "vue";
import Pop from "../utils/Pop.js";
import { logger } from "../utils/Logger.js";
import { carsService } from "../services/CarsService.js";
import { useRoute } from "vue-router";
export default {
  setup() {
    const route = useRoute();
    const editMode = ref(false);
    async function getCarById() {
      try {
        await carsService.getCarById(route.params.id);
      } catch (error) {
        Pop.error(error);
        logger.error(error);
      }
    }
    onMounted(() => {
      getCarById();
    });
    return {
      editMode,
      account: computed(() => AppState.account),
      car: computed(() => AppState.activeCar),
      async editCar() {
        editMode.value = false;
      },
    };
  },
};
</script>


<style lang="scss" scoped>
</style>
