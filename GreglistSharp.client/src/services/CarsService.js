import { AppState } from "../AppState.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"


class CarsService {

    async getCars() {
        const res = await api.get('api/cars')
        logger.log('[getCars]', res.data)
        AppState.cars = res.data
    }

    async getCarById(id) {
        const res = await api.get('api/cars/' + id)
        logger.log('[getCarById]', res.data)
        AppState.activeCar = res.data
    }

    async createCar(carData) {
        const res = await api.post('api/cars', carData)
        logger.log('[createCar]', res.data)
        // NOTE push add to the end of the list, if you want to add to the start of the list use unshift
        AppState.cars.push(res.data)
    }

    async editCar(carData) {
        logger.log(carData)
        const res = await api.put('api/cars/' + id, carData)
        logger.log('[editCar]', res.data)
    }

    async removeCar(id) {
        // logger.log('deleteing', id)
        const res = await api.delete('api/cars/' + id)
        logger.log('[removeCar]', res.data)
        // NOTE should this be only one line? NO
        // AppState.cars.splice(AppState.cars.findIndex(c => c.id == id), 1)
        // a little better cause there steps to it that can be debugged cleaner
        let index = AppState.cars.findIndex(c => c.id == id)
        if (index >= 0) {
            AppState.cars.splice(index, 1)
        }
    }

}

export const carsService = new CarsService()
