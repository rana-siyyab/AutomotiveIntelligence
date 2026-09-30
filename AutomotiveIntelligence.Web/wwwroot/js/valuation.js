document.addEventListener("DOMContentLoaded", function() {

    const makeDropdown =
        document.getElementById("MakeId");

    const modelDropdown =
        document.getElementById("ModelId");

    const variantDropdown =
        document.getElementById("VariantId");

    const countryDropdown =
        document.getElementById("CountryId");

    const provinceDropdown =
        document.getElementById("ProvinceId");

    const cityDropdown =
        document.getElementById("CityId");

    const valuationForm =
        document.getElementById("valuationForm");

    const readinessText =
        document.getElementById("estimateReadiness");

    const readinessLevel =
        document.getElementById("estimateLevel");

    const readinessMessage =
        document.getElementById("readinessMessage");

    const readinessProgress =
        document.getElementById("readinessProgress");


    /*
     * Reset a dropdown
     */

    function resetDropdown(dropdown, placeholder) {

        if (!dropdown) {
            return;
        }

        dropdown.innerHTML =
            `<option value="">${placeholder}</option>`;

        dropdown.disabled = true;
    }


    /*
     * Populate a dropdown
     */

    function populateDropdown(
        dropdown,
        items,
        valueProperty,
        textProperty,
        placeholder) {

        if (!dropdown) {
            return;
        }

        dropdown.innerHTML =
            `<option value="">${placeholder}</option>`;

        items.forEach(item => {

            const option =
                document.createElement("option");

            option.value =
                item[valueProperty];

            option.textContent =
                item[textProperty];

            dropdown.appendChild(option);
        });

        dropdown.disabled =
            items.length === 0;
    }


    /*
     * Fetch JSON
     */

    async function fetchJson(url) {

        const response =
            await fetch(url, {
                headers: {
                    "X-Requested-With": "XMLHttpRequest"
                }
            });

        if (!response.ok) {

            throw new Error(
                `Request failed: ${response.status}`
            );
        }

        return await response.json();
    }


    /*
     * Load Models
     */

    async function loadModels(
        makeId,
        selectedModelId = null) {

        resetDropdown(
            modelDropdown,
            "Select Model"
        );

        resetDropdown(
            variantDropdown,
            "Select Variant"
        );

        if (!makeId) {

            updateReadiness();

            return;
        }

        try {

            const models =
                await fetchJson(
                    `/Valuation/Models?makeId=${encodeURIComponent(makeId)}`
                );

            populateDropdown(
                modelDropdown,
                models,
                "modelId",
                "name",
                "Select Model"
            );

            if (selectedModelId) {

                const exists =
                    models.some(
                        model =>
                            Number(model.modelId) ===
                            Number(selectedModelId)
                    );

                if (exists) {

                    modelDropdown.value =
                        selectedModelId;
                }
            }

        }
        catch (error) {

            console.error(
                "Unable to load models:",
                error
            );

            resetDropdown(
                modelDropdown,
                "Unable to load models"
            );
        }

        updateReadiness();
    }


    /*
     * Load Variants
     */

    async function loadVariants(
        modelId,
        selectedVariantId = null) {

        resetDropdown(
            variantDropdown,
            "Select Variant"
        );

        if (!modelId) {

            updateReadiness();

            return;
        }

        try {

            const variants =
                await fetchJson(
                    `/Valuation/Variants?modelId=${encodeURIComponent(modelId)}`
                );

            populateDropdown(
                variantDropdown,
                variants,
                "variantId",
                "name",
                "Select Variant"
            );

            if (selectedVariantId) {

                const exists =
                    variants.some(
                        variant =>
                            Number(variant.variantId) ===
                            Number(selectedVariantId)
                    );

                if (exists) {

                    variantDropdown.value =
                        selectedVariantId;
                }
            }

        }
        catch (error) {

            console.error(
                "Unable to load variants:",
                error
            );

            resetDropdown(
                variantDropdown,
                "Unable to load variants"
            );
        }

        updateReadiness();
    }


    /*
     * Load Provinces
     */

    async function loadProvinces(
        countryId,
        selectedProvinceId = null) {

        resetDropdown(
            provinceDropdown,
            "Select Province"
        );

        resetDropdown(
            cityDropdown,
            "Select City"
        );

        if (!countryId) {

            updateReadiness();

            return;
        }

        try {

            const provinces =
                await fetchJson(
                    `/Valuation/Provinces?countryId=${encodeURIComponent(countryId)}`
                );

            populateDropdown(
                provinceDropdown,
                provinces,
                "provinceId",
                "name",
                "Select Province"
            );

            if (selectedProvinceId) {

                const exists =
                    provinces.some(
                        province =>
                            Number(province.provinceId) ===
                            Number(selectedProvinceId)
                    );

                if (exists) {

                    provinceDropdown.value =
                        selectedProvinceId;
                }
            }

        }
        catch (error) {

            console.error(
                "Unable to load provinces:",
                error
            );

            resetDropdown(
                provinceDropdown,
                "Unable to load provinces"
            );
        }

        updateReadiness();
    }


    /*
     * Load Cities
     */

    async function loadCities(
        provinceId,
        selectedCityId = null) {

        resetDropdown(
            cityDropdown,
            "Select City"
        );

        if (!provinceId) {

            updateReadiness();

            return;
        }

        try {

            const cities =
                await fetchJson(
                    `/Valuation/Cities?provinceId=${encodeURIComponent(provinceId)}`
                );

            populateDropdown(
                cityDropdown,
                cities,
                "cityId",
                "name",
                "Select City"
            );

            if (selectedCityId) {

                const exists =
                    cities.some(
                        city =>
                            Number(city.cityId) ===
                            Number(selectedCityId)
                    );

                if (exists) {

                    cityDropdown.value =
                        selectedCityId;
                }
            }

        }
        catch (error) {

            console.error(
                "Unable to load cities:",
                error
            );

            resetDropdown(
                cityDropdown,
                "Unable to load cities"
            );
        }

        updateReadiness();
    }


    /*
     * Make → Model
     */

    if (makeDropdown) {

        makeDropdown.addEventListener(
            "change",
            async function() {

                await loadModels(
                    this.value
                );

                updateReadiness();
            }
        );
    }


    /*
     * Model → Variant
     */

    if (modelDropdown) {

        modelDropdown.addEventListener(
            "change",
            async function() {

                await loadVariants(
                    this.value
                );

                updateReadiness();
            }
        );
    }


    /*
     * Country → Province
     */

    if (countryDropdown) {

        countryDropdown.addEventListener(
            "change",
            async function() {

                await loadProvinces(
                    this.value
                );

                updateReadiness();
            }
        );
    }


    /*
     * Province → City
     */

    if (provinceDropdown) {

        provinceDropdown.addEventListener(
            "change",
            async function() {

                await loadCities(
                    this.value
                );

                updateReadiness();
            }
        );
    }


    /*
     * Optional fields
     */

    const optionalFields = [

        variantDropdown,

        document.getElementById(
            "ManufacturingYear"
        ),

        cityDropdown,

        document.getElementById(
            "Mileage"
        ),

        document.getElementById(
            "ConditionId"
        ),

        document.getElementById(
            "HasAccidentHistory"
        ),

        document.getElementById(
            "OwnerCount"
        ),

        document.getElementById(
            "AskingPrice"
        )
    ];


    optionalFields.forEach(field => {

        if (!field) {
            return;
        }

        field.addEventListener(
            "change",
            updateReadiness
        );

        field.addEventListener(
            "input",
            updateReadiness
        );
    });


    /*
     * Readiness indicator
     *
     * This is UI guidance only.
     * Actual valuation rules remain
     * inside the backend.
     */

    function updateReadiness() {

        if (!makeDropdown ||
            !modelDropdown ||
            !readinessText ||
            !readinessLevel ||
            !readinessMessage ||
            !readinessProgress) {

            return;
        }


        const hasMake =
            Boolean(makeDropdown.value);

        const hasModel =
            Boolean(modelDropdown.value);


        if (!hasMake || !hasModel) {

            readinessText.textContent =
                "Select Make and Model to begin";

            readinessLevel.textContent =
                "Waiting";

            readinessLevel.className =
                "readiness-badge";

            readinessMessage.textContent =
                "You can start with only the required vehicle identity.";

            readinessProgress.style.width =
                "10%";

            return;
        }


        let optionalCount = 0;


        optionalFields.forEach(field => {

            if (field && field.value) {

                optionalCount++;
            }
        });


        if (optionalCount === 0) {

            readinessText.textContent =
                "Quick Estimate Ready";

            readinessLevel.textContent =
                "Basic";

            readinessLevel.className =
                "readiness-badge basic";

            readinessMessage.textContent =
                "Add the manufacturing year for a more specific estimate.";

            readinessProgress.style.width =
                "30%";

            return;
        }


        if (optionalCount <= 2) {

            readinessText.textContent =
                "Standard Estimate Ready";

            readinessLevel.textContent =
                "Standard";

            readinessLevel.className =
                "readiness-badge standard";

            readinessMessage.textContent =
                "Adding more vehicle details can further narrow the market range.";

            readinessProgress.style.width =
                "55%";

            return;
        }


        readinessText.textContent =
            "Detailed Estimate Ready";

        readinessLevel.textContent =
            "Detailed";

        readinessLevel.className =
            "readiness-badge detailed";

        readinessMessage.textContent =
            "Your additional vehicle information will help provide a more specific market assessment.";

        readinessProgress.style.width =
            "85%";
    }


    /*
     * Restore previous selections after
     * server-side validation failure.
     */

    async function restoreSelections() {

        if (!makeDropdown) {
            return;
        }


        const selectedMakeId =
            makeDropdown.value;

        const selectedModelId =
            modelDropdown?.dataset.selectedValue || "";

        const selectedVariantId =
            variantDropdown?.dataset.selectedValue || "";

        const selectedCountryId =
            countryDropdown?.value || "";

        const selectedProvinceId =
            provinceDropdown?.dataset.selectedValue || "";

        const selectedCityId =
            cityDropdown?.dataset.selectedValue || "";


        /*
         * Restore Make → Model → Variant
         */

        if (selectedMakeId) {

            await loadModels(
                selectedMakeId,
                selectedModelId
            );


            if (modelDropdown?.value) {

                await loadVariants(
                    modelDropdown.value,
                    selectedVariantId
                );
            }
        }


        /*
         * Restore Country → Province → City
         */

        if (selectedCountryId) {

            await loadProvinces(
                selectedCountryId,
                selectedProvinceId
            );


            if (provinceDropdown?.value) {

                await loadCities(
                    provinceDropdown.value,
                    selectedCityId
                );
            }
        }


        updateReadiness();
    }


    /*
     * Basic client-side form protection
     */

    if (valuationForm) {

        valuationForm.addEventListener(
            "submit",
            function(event) {

                if (!makeDropdown.value) {

                    event.preventDefault();

                    makeDropdown.focus();

                    return;
                }


                if (!modelDropdown.value) {

                    event.preventDefault();

                    modelDropdown.focus();

                    return;
                }
            }
        );
    }


    /*
     * Initial setup
     */

    restoreSelections();

    updateReadiness();

});