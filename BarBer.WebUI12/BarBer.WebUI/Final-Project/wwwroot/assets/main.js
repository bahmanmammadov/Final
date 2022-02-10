(function () {
    "use strict";
  
    var carousels = function () {
      $(".owl-carousel1").owlCarousel({
        loop: true,
        center: true,
        margin: 0,
        responsiveClass: true,
        nav: false,
        responsive: {
          0: {
            items: 1,
            nav: false
          },
          680: {
            items: 2,
            nav: false,
            loop: false
          },
          1000: {
            items: 3,
            nav: true
          }
        }
      });
    };
  
    (function ($) {
      carousels();
    })(jQuery);
  })();
  
  
  'use strict'

class Accordion {
  constructor(element, options = {}) {
    this.accordion = element
    this.buttons = null
    this.bodies = null
    this.options = {
      activeClassName: 'is-active',
      closeOthers: true,
      ...options,
    }

    this.handleKeydown = this.handleKeydown.bind(this)
    this.handleClick = this.handleClick.bind(this)
    this.handleTransitionend = this.handleTransitionend.bind(this)

    this.init()
  }

  init() {
    if (this.accordion.classList.contains('is-init-accordion')) {
      throw Error('Accordion is already initialized.')
    }

    this.buttons = [...this.accordion.querySelectorAll('.js-accordion__button')]
    this.bodies = [...this.accordion.querySelectorAll('.js-accordion__body')]

    // Handle active accordion item
    for (const button of this.buttons) {
      if (!button.classList.contains(this.options.activeClassName)) continue
      button.setAttribute('aria-expanded', 'true')
      const body = button.nextElementSibling
      body.style.display = 'block'
      body.style.maxHeight = 'none'
    }

    // Hide all bodies except the active
    for (const body of this.bodies) {
      if (body.previousElementSibling.classList.contains(this.options.activeClassName)) continue
      body.style.display = 'none'
      body.style.maxHeight = '0px'
    }

    this.addEvents()

    this.accordion.classList.add('is-init-accordion')
  }

  closeOthers(elException) {
    for (const button of this.buttons) {
      if (button === elException) continue
      button.classList.remove(this.options.activeClassName)
      button.setAttribute('aria-expanded', 'false')
      const body = button.nextElementSibling
      body.style.maxHeight = `${body.scrollHeight}px`
      setTimeout(() => void (body.style.maxHeight = '0px'), 0)
    }
  }

  handleKeydown(event) {
    const target = event.target
    const key = event.which.toString()

    if (target.classList.contains('js-accordion__button') && key.match(/35|36|38|40/)) {
      event.preventDefault()
    } else {
      return false
    }

    switch (key) {
      case '36':
        // "Home" key
        this.buttons[0].focus()
        break
      case '35':
        // "End" key
        this.buttons[this.buttons.length - 1].focus()
        break
      case '38':
        // "Up Arrow" key
        const prevIndex = this.buttons.indexOf(target) - 1
        if (this.buttons[prevIndex]) {
          this.buttons[prevIndex].focus()
        } else {
          this.buttons[this.buttons.length - 1].focus()
        }
        break
      case '40':
        // "Down Arrow" key
        const nextIndex = this.buttons.indexOf(target) + 1
        if (this.buttons[nextIndex]) {
          this.buttons[nextIndex].focus()
        } else {
          this.buttons[0].focus()
        }
        break
    }
  }

  handleClick(event) {
    const button = event.currentTarget
    const body = button.nextElementSibling

    if (this.options.closeOthers) {
      this.closeOthers(button)
    }

    // Set height to the active body
    if (body.style.maxHeight === 'none') {
      body.style.maxHeight = `${body.scrollHeight}px`
    }

    if (button.classList.contains(this.options.activeClassName)) {
      // Close accordion item
      button.classList.remove(this.options.activeClassName)
      button.setAttribute('aria-expanded', 'false')
      setTimeout(() => void (body.style.maxHeight = '0px'), 0)
    } else {
      // Open accordion item
      button.classList.add(this.options.activeClassName)
      button.setAttribute('aria-expanded', 'true')
      body.style.display = 'block'
      body.style.maxHeight = `${body.scrollHeight}px`
    }
  }

  handleTransitionend(event) {
    const body = event.currentTarget
    if (body.style.maxHeight !== '0px') {
      // Remove the height from the active body
      body.style.maxHeight = 'none'
    } else {
      // Hide the active body
      body.style.display = 'none'
    }
  }

  addEvents() {
    this.accordion.addEventListener('keydown', this.handleKeydown)
    for (const button of this.buttons) {
      button.addEventListener('click', this.handleClick)
    }
    for (const body of this.bodies) {
      body.addEventListener('transitionend', this.handleTransitionend)
    }
  }

  destroy() {
    this.accordion.removeEventListener('keydown', this.handleKeydown)
    for (const button of this.buttons) {
      button.removeEventListener('click', this.handleClick)
    }
    for (const body of this.bodies) {
      body.addEventListener('transitionend', this.handleTransitionend)
    }

    this.buttons = null
    this.bodies = null

    this.accordion.classList.remove('is-init-accordion')
  }
}

// ---

window.addEventListener('DOMContentLoaded', () => {
    const accordionEls = [...document.querySelectorAll('.js-accordion')]
    for (const accordionEl of accordionEls) {
        new Accordion(accordionEl)
    }
})