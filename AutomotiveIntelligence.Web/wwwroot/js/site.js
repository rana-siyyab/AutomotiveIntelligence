document.addEventListener(
    "DOMContentLoaded",
    function () {

        /*
         * Automotive Intelligence Home
         *
         * Lightweight interaction layer.
         * Navigation remains server-side through Razor
         * tag helpers.
         */

        const roadmapItems =
            document.querySelectorAll(
                ".ai-roadmap-item"
            );


        if (roadmapItems.length > 0) {

            roadmapItems.forEach(
                function (item, index) {

                    item.style.animationDelay =
                        `${index * 0.08}s`;

                }
            );

        }


        /*
         * Subtle hero visual animation.
         */

        const vehicleCard =
            document.querySelector(
                ".ai-vehicle-card"
            );


        if (vehicleCard) {

            let direction = 1;
            let offset = 0;


            function animateVehicleCard() {

                offset +=
                    direction * 0.025;


                if (offset > 2) {
                    direction = -1;
                }


                if (offset < -2) {
                    direction = 1;
                }


                vehicleCard.style.transform =
                    `translateY(${offset}px)`;


                window.requestAnimationFrame(
                    animateVehicleCard
                );
            }


            animateVehicleCard();

        }

    }
);