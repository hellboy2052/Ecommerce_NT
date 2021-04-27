import * as images from "../contains/images";

const initialState = {
    
};

export default (state = initialState, { type, payload }) => {
    switch (type) {
        case images.POST_IMAGES: {
            return { ...state };

        }
        default:
            return state;
}
}