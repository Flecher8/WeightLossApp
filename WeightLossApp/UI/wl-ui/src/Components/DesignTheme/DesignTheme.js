import { useState, useEffect } from "react";
import React from "react";
import { constants } from "../../Constants";
import { Modal, Button, Form, Row, Col, Table, InputGroup, FormControl } from "react-bootstrap";

function DesignThemeData() {
    // Design theme data
    const [designThemes, setDesignThemes] = useState([]);
    const [designTheme, setDesignTheme] = useState();

    const [addShow, setAddShow] = useState(false);
	const addHandleClose = () => setAddShow(false);
	const addHandleShow = () => setAddShow(true);

    const [editShow, setEditShow] = useState(false);
	const editHandleClose = () => setEditShow(false);
	const editHandleShow = () => setEditShow(true);

    const [search, setSearch] = useState("");
	const [filterParameters, setFilterParameters] = useState({
		BaseColor: false,
		SecondaryColor: false,
		AccentColor: false
	});

    // Get exercises data
	function getDesignThemes() {
		fetch(constants.API_URL + "DesignThemeData")
			.then(res => res.json())
			.then(data => setDesignThemes(data));
	}

	// onLoad function
	useEffect(() => {
		getDesignThemes();
	}, []);

    // Show add modal function
	function addModalShow() {
		addHandleShow();
		setDesignTheme({
			Id: undefined,
			BaseColor: "",
			SecondaryColor: "",
            AccentColor: "",
			IconImage: ""
		});
	}

    // Show edit modal function
	function editModalShow(exercise) {
		editHandleShow();
		setDesignTheme(exercise);
	}

    // Delete item function
	function deleteClick(id) {
		if (window.confirm("Are you sure?")) {
			fetch(constants.API_URL + `DesignThemeData/${id}`, {
				method: "DELETE",
				headers: {
					Accept: "application/json",
					"Content-Type": "application/json"
				}
			})
				.then(res => res.json())
				.then(
					result => {
						getDesignThemes();
					},
					error => {
						alert("Failed");
					}
				);
		}
	}

    // Seact by color
	function searchByColor(color) {
		let arr = [];
		for (let i = 0; i < designThemes.length; i++) {
			if (designThemes[i].BaseColor.match(color)
            || designThemes[i].SecondaryColor.match(color)
            || designThemes[i].AccentColor.match(color)) {
				arr.push(designThemes[i]);
			}
		}
		setDesignThemes(arr);
	}

    // Sort functions
	function sortByBaseColor() {
		if (filterParameters.BaseColor) {
			designThemes.sort((a, b) => a.BaseColor.localeCompare(b.BaseColor));
			setFilterParameters({
				BaseColor: false,
				SecondaryColor: filterParameters.SecondaryColor,
				AccentColor: filterParameters.AccentColor
			});
		} else {
			designThemes.sort((b, a) => a.BaseColor.localeCompare(b.BaseColor));
			setFilterParameters({
				BaseColor: true,
				SecondaryColor: filterParameters.SecondaryColor,
				AccentColor: filterParameters.AccentColor
			});
		}
	}
    function sortBySecondaryColor() {
		if (filterParameters.Name) {
			designThemes.sort((a, b) => a.SecondaryColor.localeCompare(b.SecondaryColor));
			setFilterParameters({
				BaseColor: filterParameters.BaseColor,
				SecondaryColor: false,
				AccentColor: filterParameters.AccentColor,
			});
		} else {
			designThemes.sort((b, a) => a.SecondaryColor.localeCompare(b.SecondaryColor));
			setFilterParameters({
				BaseColor: filterParameters.BaseColor,
				SecondaryColor: true,
				AccentColor: filterParameters.AccentColor,
			});
		}
	}
    function sortByAccentColor() {
		if (filterParameters.Name) {
			designThemes.sort((a, b) => a.AccentColor.localeCompare(b.AccentColor));
			setFilterParameters({
				BaseColor: filterParameters.BaseColor,
				SecondaryColor: filterParameters.SecondaryColor,
				AccentColor: false
			});
		} else {
			designThemes.sort((b, a) => a.AccentColor.localeCompare(b.AccentColor));
			setFilterParameters({
				BaseColor: filterParameters.BaseColor,
				SecondaryColor: filterParameters.SecondaryColor,
				AccentColor: true
			});
		}
	}
	return (
		<div className="DesignThemes container">
			{/* Create new item button */}
			<div className="container mt-5" align="right">
				<Button onClick={() => addModalShow()} variant="outline-primary">
					Create new design theme
				</Button>
			</div>
			{/* Search */}
			<div className="container mt-5">
				<InputGroup className="mb-3">
					<FormControl
						aria-label="Default"
						placeholder="Search"
						value={search}
						onChange={e => setSearch(e.target.value)}
						aria-describedby="inputGroup-sizing-default"
					/>
					<Button onClick={() => searchByColor(search)}>Search</Button>
					<Button onClick={() => getDesignThemes()}>Cancel</Button>
				</InputGroup>
			</div>
		</div>
	)
}

export default DesignThemeData;